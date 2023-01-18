using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Resources
{
    public class Result<TEntity> where TEntity : class
    {
        public List<TEntity> Entities { get; set; }
        public TEntity Entity { get; set; }
        public Meta Meta { get; set; }
    }

    public class Meta
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string ErrorMessage { get; set; }
    }
}
