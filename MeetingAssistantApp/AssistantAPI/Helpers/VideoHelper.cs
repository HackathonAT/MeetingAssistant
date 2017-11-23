using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssistantAPI.Helpers
{
    public static class VideoHelper
    {
        /// <summary>
        /// calls the video indexer API and gets the video breakdown for the video with given id
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns>video breakdown as json</returns>
        public static string GetVideoBreakdown(string videoId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the video with the given id
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns>true if deleted, false otherwise</returns>
        public static bool DeleteVideo(string videoId)
        {
            throw new NotImplementedException();
        }

        public static string UploadVideo(byte[] videoContent)
        {
            throw new NotImplementedException();
        }
    }
}