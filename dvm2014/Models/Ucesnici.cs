using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace dvm2014.Models
{
    public class UcesniciContext : DbContext
    {
        public UcesniciContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Ucesnici> Ucesnici { get; set; }
    }

    [Table("Ucesnici")]
    public class Ucesnici
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UcesnikID { get; set; }
        [Required(ErrorMessage="Не внесовте име и презиме")]
        [StringLength(200)]
        public string ImePrezime { get; set; }
        [StringLength(200)]
        public string Organizacija { get; set; }
        [StringLength(20)]
        public string Telefon { get; set; }
        [StringLength(150)]
        public string Eposta { get; set; }
        [StringLength(50)]
        public string Grad { get; set; }
        [StringLength(50)]
        public string Drzava { get; set; }
        [StringLength(30)]
        public string Drzavjanin { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public int Ucestvo { get; set; }
        [StringLength(30)]
        public string NacinUplata { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public int Smestuvanje { get; set; }
        [StringLength(30)]
        public string NacinUplataS { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public int NacVecera { get; set; }
        [StringLength(30)]
        public string NacinUplataN { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public int Provizija { get; set; }
        [StringLength(30)]
        public string NacinUplataP { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public int DopoTrosoci { get; set; }
        [StringLength(30)]
        public string NacinUplataD { get; set; }
        [StringLength(5)]
        public string Valuta { get; set; }
        [StringLength(1)]
        public string Pecateno { get; set; }
        [StringLength(200)]
        public string Zabeleska { get; set; }
        [StringLength(1)]
        public string sertifikat { get; set; }
    }
}