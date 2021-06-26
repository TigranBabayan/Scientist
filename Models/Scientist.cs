using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace scientist.Models
{
    public class Scientist
    {
        public int Id { get; set; }

        [DisplayName("Ա․Ա․Հ․")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Ծննդյան օր")]
        public DateTime? Birthday { get; set; }
        [DisplayName("Քաղաքացիություն")]
        public string Cityzenship { get; set; }
        [DisplayName("Մասնագիտություն")]
        public string Profession { get; set; }

        [DisplayName("Աշհ․ վայր")]
        public string Workplace { get; set; }

        [DisplayName("Բնակ․ վայր")]

        public string Livingplace { get; set; }
        [DisplayName("Գիտ․ կոչում")]
        public string Degree { get; set; }
        [DisplayName("Բնակ․ հասցե")]
        public string Adress { get; set; }
        [DisplayName("Հեռախոս․")]
        public string Phone { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }
      
        [DisplayName("Լրացուցիչ դաշտ")]
        public string Other { get; set; }

    }
}
