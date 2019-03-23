namespace Client
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    internal static class Monitoring
    {
        #region methods

        internal static float GetTemperature()
        {
            var result = "";
#if DEBUG
            result = new Random().Next(30, 70) + ".3'C";
#else
            // bash command / opt / vc / bin / vcgencmd measure_temp
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"/opt/vc/bin/vcgencmd measure_temp\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

#endif
            var temperatureResult = result.Substring(result.IndexOf('=') + 1, result.IndexOf("'") - (result.IndexOf('=') + 1)).Replace('.', ',');
            var temperature = 0.0f;
            if (float.TryParse(temperatureResult, out temperature))
            {
                return temperature;
            }
            return 0.0f;
        }

        #endregion
    }
}