using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Controls;
using RolandK.AvaloniaExtensions.ExceptionHandling.Data;

namespace RolandK.AvaloniaExtensions.ExceptionHandling;

public static class GlobalErrorReporting
{
    /// <summary>
    /// Shows an error dialog in the current process.
    /// </summary>
    /// <param name="exception">The exception to be shown to the user.</param>
    /// <param name="parentWindow">The parent window for the error dialog.</param>
    /// <param name="exceptionAnalyzers">If null, a default collection of IExceptionAnalyzers ist used.</param>
    public static async Task ShowGlobalExceptionDialogAsync(
        Exception exception, 
        Window parentWindow,
        IEnumerable<IExceptionAnalyzer>? exceptionAnalyzers = null)
    {
        var exceptionInfo = new ExceptionInfo(exception, exceptionAnalyzers);
        
        var dialog = new UnexpectedErrorDialog();
        dialog.DataContext = exceptionInfo;
        await dialog.ShowDialog(parentWindow);
    }
    
    /// <summary>
    /// Tries to show an error dialog with some exception details.
    /// If it is not possible for any reason, this method simply does nothing.
    ///
    /// This call is a blocking call. It es meant to be called within a global
    /// try-catch block in Program.cs
    /// </summary>
    /// <param name="exception">The exception to be shown to the user.</param>
    /// <param name="applicationTempDirectoryName">This should be a technical name, the method uses it to create a temporary directory in the filesystem.</param>
    /// <param name="exceptionViewerExecutableProjectName">The project name of the executable showing the error dialog.</param>
    /// <param name="exceptionAnalyzers">If null, a default collection of IExceptionAnalyzers ist used.</param>
    public static void TryShowBlockingGlobalExceptionDialogInAnotherProcess(
        Exception exception, 
        string applicationTempDirectoryName,
        string exceptionViewerExecutableProjectName,
        IEnumerable<IExceptionAnalyzer>? exceptionAnalyzers = null)
    {
        try
        {
            // Write exception details to a temporary file
            var errorDirectoryPath = GetErrorFileDirectoryAndEnsureCreated(applicationTempDirectoryName);
            var errorFilePath = GenerateErrorFilePath(errorDirectoryPath);
            
            WriteExceptionInfoToFile(exception, exceptionAnalyzers, errorFilePath);
            try
            {
                if (!TryFindViewerExecutable(exceptionViewerExecutableProjectName, out var executablePath))
                {
                    return;
                }
                
                ShowGlobalException(errorFilePath, executablePath);
            }
            finally
            {
                // Delete the temporary file
                File.Delete(errorFilePath);
            }
        }
        catch(Exception)
        {
            // Nothing to do here..
        }
    }
    
    private static string GetErrorFileDirectoryAndEnsureCreated(string applicationTempDirectoryName)
    {
        var errorDirectoryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            applicationTempDirectoryName);
        if (!Directory.Exists(errorDirectoryPath))
        {
            Directory.CreateDirectory(errorDirectoryPath);
        }
        return errorDirectoryPath;
    }

    private static string GenerateErrorFilePath(string errorDirectoryPath)
    {
        string errorFilePath;
        do
        {
            var errorGuid = Guid.NewGuid();
            errorFilePath = Path.Combine(
                errorDirectoryPath,
                $"Error-{errorGuid}.err");
        } while (File.Exists(errorFilePath));

        return errorFilePath;
    }

    private static void WriteExceptionInfoToFile(
        Exception exception, 
        IEnumerable<IExceptionAnalyzer>? exceptionAnalyzers,
        string targetFileName)
    {
        using var outStream = File.Create(targetFileName);
        
        var exceptionInfo = new ExceptionInfo(exception, exceptionAnalyzers);
        JsonSerializer.Serialize(
            outStream, 
            exceptionInfo, 
            new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
    }
    
    private static bool TryFindViewerExecutable(
        string exceptionViewerExecutableProjectName,
        out string executablePath)
    {
        executablePath = string.Empty;

        var executingAssembly = Assembly.GetExecutingAssembly();
        var executingAssemblyDirectory = Path.GetDirectoryName(executingAssembly.Location);
        if (string.IsNullOrEmpty(executingAssemblyDirectory) ||
            string.IsNullOrEmpty(exceptionViewerExecutableProjectName))
        {
            return false;
        }
        
        var executablePathCheck = Path.Combine(executingAssemblyDirectory, exceptionViewerExecutableProjectName);
        if (!File.Exists(executablePathCheck))
        {
            executablePathCheck += ".exe";
            if (!File.Exists(executablePathCheck))
            {
                return false;
            }
        }

        executablePath = executablePathCheck;
        return true;
    }
    
    private static void ShowGlobalException(string exceptionDetailsFilePath, string executablePath)
    {
        var processStartInfo = new ProcessStartInfo(
            executablePath,
            $"\"{exceptionDetailsFilePath}\"");
        processStartInfo.ErrorDialog = false;
        processStartInfo.UseShellExecute = false;

        var childProcess = Process.Start(processStartInfo);
        if (childProcess != null)
        {
            childProcess.WaitForExit();
        }
    }
}