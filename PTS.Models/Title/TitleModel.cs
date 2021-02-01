using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.Title
{
    public class TitleModel:IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
