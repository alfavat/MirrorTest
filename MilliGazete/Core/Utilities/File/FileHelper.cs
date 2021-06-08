using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;

namespace Core.Utilities.File
{
    public class FileHelper : IFileHelper
    {
        public IConfiguration _configuration { get; }
        public FileHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool OptimizeVideo(string videoFileName)
        {
                if (videoFileName.StringIsNullOrEmpty())
                    return false;
                string temp_file;
                videoFileName = Path.Combine(Directory.GetCurrentDirectory(), videoFileName);
                var ffmpegPath = _configuration.GetSection("FFmpegPath").Get<string>();
                int videoWidth, videoHeight;
                string[] videoFrameSize;
                var inputFile = new MediaFile { Filename = videoFileName };
                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                }
                if (inputFile != null && inputFile.Metadata != null && inputFile.Metadata.VideoData != null && !string.IsNullOrEmpty(inputFile.Metadata.VideoData.FrameSize))
                {
                    videoFrameSize = inputFile.Metadata.VideoData.FrameSize.ToLower().Trim().Split('x');
                    if (videoFrameSize != null && videoFrameSize.Length > 1)
                    {
                        videoWidth = Convert.ToInt32(videoFrameSize[0]);
                        videoHeight = Convert.ToInt32(videoFrameSize[1]);
                        if (inputFile.Metadata.VideoData.Fps > 25 || (videoWidth > 1920 && videoHeight > 1080))
                        {
                            var startInfo = new ProcessStartInfo();
                            startInfo.CreateNoWindow = false;
                            startInfo.UseShellExecute = false;
                            startInfo.FileName = ffmpegPath;
                            startInfo.WindowStyle = ProcessWindowStyle.Normal;

                            var fileName = Path.GetFileName(videoFileName);
                            temp_file = videoFileName;
                            temp_file = temp_file.Replace(fileName, "temp_" + fileName);
                            var optimizedWidth = videoWidth * 1080 / videoHeight;
                            startInfo.Arguments = (videoWidth != 1920 && videoHeight > 1080 && inputFile.Metadata.VideoData.Fps > 25) ? $"-i {videoFileName} -vf scale={optimizedWidth}:1080 -r 25 {temp_file}"
                            : inputFile.Metadata.VideoData.Fps > 25 ? $"-i {videoFileName} -r 25 {temp_file}" : $"-i {videoFileName} -vf scale={optimizedWidth}:1080 {temp_file}";
                            if (System.IO.File.Exists(temp_file))
                                System.IO.File.Delete(temp_file);
                            using (Process exeProcess = Process.Start(startInfo))
                            {
                                exeProcess.WaitForExit();
                            }

                            if (System.IO.File.Exists(videoFileName) && System.IO.File.Exists(temp_file))
                            {
                                System.IO.File.Delete(videoFileName);
                                System.IO.File.Move(temp_file, videoFileName);
                            }
                        }
                    }
                }

                var bitrateSize = _configuration.GetSection("MaximumBitRateSize").Get<int>();
                if (inputFile != null && inputFile.Metadata != null && inputFile.Metadata.VideoData != null &&
                   inputFile.Metadata.VideoData.BitRateKbs.HasValue && inputFile.Metadata.VideoData.BitRateKbs.Value > bitrateSize)
                {
                    var optimizedBitRate = _configuration.GetSection("OptimizedBitRate").Get<string>();

                    var startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = ffmpegPath;
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;

                    var fileName = Path.GetFileName(videoFileName);
                    temp_file = videoFileName;
                    temp_file = temp_file.Replace(fileName, "temp_" + fileName);
                    //ffmpeg -i input.mp4 -b:v 6M  output.mp4
                    startInfo.Arguments = $"-i {videoFileName} -b:v {optimizedBitRate}M {temp_file}";
                    if (System.IO.File.Exists(temp_file))
                        System.IO.File.Delete(temp_file);
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }

                    if (System.IO.File.Exists(videoFileName) && System.IO.File.Exists(temp_file))
                    {
                        System.IO.File.Delete(videoFileName);
                        System.IO.File.Move(temp_file, videoFileName);
                    }
                }
                return true;
        }
    }
}
