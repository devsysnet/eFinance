using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eFinance.Models
{
    [Table("mstKaryawan")]
    public class mstKaryawan
    {
        [Key]
        public int noKaryawan { get; set; }
        public string nama { get; set; }

        public string email { get; set; }
        public string password { get; set; }

        public int nocabang { get; set; }
        [Column("jabatan")]
        public int nojabatan { get; set; } // no jabatan

        public virtual mCabang Cabang { get; set; }
        public virtual mstJabatan Jabatan { get; set; }
    }
}