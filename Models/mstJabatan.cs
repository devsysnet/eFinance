using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eFinance.Models
{
    [Table("mstJabatan")]
    public class mstJabatan
    {
        [Key]
        public int noJabatan { get; set; }
        public string Jabatan { get; set; }

        public List<mstKaryawan> karyawans { get; set; }
    }
}