using DataAccess;
using DataAccess.Interfaces;
using System;

namespace AnalysisModel
{
    public class DocumentAnalysisResult : IEntity
    {
        public Key Key { get; set; }

        public CharacterStats CharacterStats { get; set; }
    }
}
