using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBStorage
{
    public class SqlBookReview
    {
        public int BookId { get; set; }
        public float ReviewScore { get; set; }
        public string ReviewText { get; set; }
    }
}
