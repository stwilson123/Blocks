﻿using Blocks.Framework.Services.DataTransfer;

namespace Blocks.Framework.Data.Combobox
{
    public class ComboboxData
    {
        [DataTransfer("id")]
        public string Id { get; set; }
        
        [DataTransfer("text")]
        public string Text { get; set; }
        
        

    }
}