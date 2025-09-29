using System.Diagnostics;

namespace Cads.Ingester.Tests.Integration.Helpers;

public static class ContainerLoggingUtility
{
    public const string ServiceNameApi = "cads_ingester";
    public const string ServiceNameLocalstack = "cadsingstr-localstack-emulator";

    public static async Task<bool> FindContainerLogEntryAsync(string containerServiceName, string entryToMatch)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "docker",
                Arguments = $"logs {containerServiceName}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        var logs = await process.StandardOutput.ReadToEndAsync();
        process.WaitForExit();

        return logs.Contains(entryToMatch);
    }
}