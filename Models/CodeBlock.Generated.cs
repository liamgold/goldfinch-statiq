// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System;
using System.Collections.Generic;
using Kentico.Kontent.Delivery.Abstractions;

namespace Goldfinch.Models
{
    public partial class CodeBlock
    {
        public const string Codename = "code_block";
        public const string CodeCodename = "code";
        public const string LanguageCodename = "language";

        public string Code { get; set; }
        public IEnumerable<IMultipleChoiceOption> Language { get; set; }
        public IContentItemSystemAttributes System { get; set; }
    }
}