using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Constanst
{
    public class Constant
    {
        public static string POSITIONSTARTPROJECT = "Project Manager";
        public static string CREATEROLE = "create";
        public static List<string> COORDINATE_STATUSES = new List<string>() {
                     "NOTYET",
                   "ONPROGRESS",
                    "REJECT",
                    "CONFIRM",
        };

		public static List<string> SPONSOR_TYPES = new List<string>() {
					 "PERSONAL",
				   "ORGANIZATION",
					"COMPANY"
		};

	}
}
