using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OKS_Tomasos.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Bestallning = new HashSet<Bestallning>();
        }

        public int KundId { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9_ ]*$", ErrorMessage = "Använd inte special tecken.")]
        [StringLength(100, ErrorMessage = "Namn får endast vara max 100 tecken")]
        public string Namn { get; set; }
        

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9_ ]*$", ErrorMessage = "Använd inte special tecken.")]
        [StringLength(50, ErrorMessage = "Gatuadress får endast vara max 50 tecken")]
        public string Gatuadress { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^[0-9_ ]*$", ErrorMessage = "Endast Siffror.")]
        [StringLength(20, ErrorMessage = "Postnummer får endast vara max 20 tecken")]
        public string Postnr { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9_ ]*$", ErrorMessage = "Använd inte special tecken.")]
        [StringLength(100, ErrorMessage = "Postort får endast vara max 100 tecken")]
        public string Postort { get; set; }

        [EmailAddress(ErrorMessage = "Du har inte matat in korrekt email.")]
        [StringLength(50, ErrorMessage = "Email får endast vara max 50 tecken")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "*")]
        [RegularExpression(@"^[0-9_ ]*$", ErrorMessage = "Endast Siffror.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9_ ]*$", ErrorMessage = "Använd inte special tecken.")]
        [StringLength(20, ErrorMessage = "Användarnamn får endast vara max 20 tecken")]
        public string AnvandarNamn { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Lösenord får endast vara max 20 tecken")]   
        public string Losenord { get; set; }

        public virtual ICollection<Bestallning> Bestallning { get; set; }
    }
}
