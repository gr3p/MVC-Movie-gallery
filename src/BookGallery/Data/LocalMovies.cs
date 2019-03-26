using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MovieGallery.Data
{
    public class LocalMovies
    {
        private string path = @"c:\temp";

        public List<string> GetMoviesfromDirList()
        {
            var list = Directory.GetDirectories(path).ToList();

            return list.Select(item => item.Remove(0, path.Length).Remove(0, path.LastIndexOf('\\')-1)).ToList();
            
        }

        public List<string> CleanReleaseNames(List<string> movieList)
        {
            var output = new List<string>();
            string pattern = @"(19|20)\d{2}";
            foreach (var movieTitle in movieList)
            {
                int index = Regex.Match(movieTitle, pattern).Index ;
                output.Add( movieTitle.Substring(0, index).Replace('.', ' '));

            }
            return output;
        }


    }
}