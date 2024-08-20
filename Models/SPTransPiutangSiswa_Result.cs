using System;

namespace eFinance.Models
{
    public class SPTransPiutangSiswa_Result
    {
        public int payment_id { get; set; }
        public int student_id { get; set; }
        public int transaction_code_id { get; set; }
        public DateTime due_date { get; set; }
        public Decimal charge { get; set; }
        public Decimal already_paid { get; set; }
        public Decimal status { get; set; }
        public string tahunajaran { get; set; }
        public string jenisTransaksi { get; set; }
        public string linkUrl { get; set; }
    }
}