using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.Json.Serialization;

namespace Base.Client.Entity
{
    public class UpgradeFileModel
    {
        /*
        {
            "fileId": 2,
            "fileName": "Halcon算子手册(中文复印版)",
            "fileMd5": "123",
            "filePath": "WebFiles\\UpgradeFiles\\Halcon算子手册(中文复印版).pdf",
            "uploadTime": "1970-01-01 03:05:11",
            "state": 1,
            "length": 64259
          }
         */
        [JsonPropertyName("fileId")]// JsonPropery
        public int FileId { get; set; }
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }
        [JsonPropertyName("fileMd5")]
        public string FileMd5 { get; set; }
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; }
        [JsonPropertyName("uploadTime")]
        public DateTime UploadTime { get; set; }
        [JsonPropertyName("state")]
        public int state { get; set; }
        [JsonPropertyName("length")]
        public int Length { get; set; }
    }
}
