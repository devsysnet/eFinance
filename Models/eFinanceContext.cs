using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace eFinance.Models
{
    public class eFinanceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public eFinanceContext() : base("name=ConnectionString")
        {
        }

        public System.Data.Entity.DbSet<eFinance.Models.TransPiutang> TransPiutangs { get; set; }

        public System.Data.Entity.DbSet<eFinance.Models.mSiswa> mSiswas { get; set; }

        public System.Data.Entity.DbSet<eFinance.Models.mCabang> mCabangs { get; set; }

        public System.Data.Entity.DbSet<eFinance.Models.mBarang> mBarangs { get; set; }

        public System.Data.Entity.DbSet<eFinance.Models.mKategori> mKategoris { get; set; }

        public System.Data.Entity.DbSet<eFinance.Models.transKelas> transKelase { get; set; }

        public System.Data.Entity.DbSet<eFinance.Models.mstKaryawan> mstKaryawans { get; set; }
        public System.Data.Entity.DbSet<eFinance.Models.tAbsensi> tAbsensis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mstKaryawan>()
                .HasRequired(e => e.Cabang)
                .WithMany(e => e.karyawans)
                .HasForeignKey(e => e.nocabang);

            modelBuilder.Entity<mstKaryawan>()
                .HasRequired(e => e.Jabatan)
                .WithMany(e => e.karyawans)
                .HasForeignKey(e => e.nojabatan);
        }
    

        //public virtual ObjectResult<SPTransPiutangSiswa_Result> SPTransPiutangSiswa()
        //{
        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPTransPiutangSiswa_Result>("SPTransPiutangSiswa");
        //}

        //public virtual ObjectResult<SPTransPiutangSiswa_Result> SPTransPiutangSiswa(string tahunajaran, Nullable<int> nosiswa)
        //{
        //    var tahunajaranParameter = tahunajaran != null ?
        //        new ObjectParameter("tahunajaran", tahunajaran) :
        //        new ObjectParameter("tahunajaran", typeof(string));

        //    var nosiswaParameter = nosiswa.HasValue ?
        //        new ObjectParameter("nosiswa", nosiswa) :
        //        new ObjectParameter("nosiswa", typeof(int));

        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPTransPiutangSiswa_Result>("SPTransPiutangSiswa", tahunajaranParameter, nosiswaParameter);
        //}

        public virtual ObjectResult<mSiswa> APICabangSiswa(Nullable<int> nocabang, Nullable<int> nosiswa)
        {
            var nocabangParameter = nocabang.HasValue ?
                new ObjectParameter("nocabang", nocabang) :
                new ObjectParameter("nocabang", typeof(int));

            var nosiswaParameter = nosiswa.HasValue ?
                new ObjectParameter("nosiswa", nosiswa) :
                new ObjectParameter("nosiswa", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<mSiswa>("APICabangSiswa", nocabangParameter, nosiswaParameter);
        }

        public virtual ObjectResult<mBarang> APICabangBarang(Nullable<int> nocabang, Nullable<int> nobarang)
        {
            var nocabangParameter = nocabang.HasValue ?
                new ObjectParameter("nocabang", nocabang) :
                new ObjectParameter("nocabang", typeof(int));

            var nobarangParameter = nobarang.HasValue ?
                new ObjectParameter("nobarang", nobarang) :
                new ObjectParameter("nobarang", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<mBarang>("APICabangBarang", nocabangParameter, nobarangParameter);
        }

        public virtual ObjectResult<mBarang> APICabangBarangGrade(Nullable<int> nocabang, string grade)
        {
            var nocabangParameter = nocabang.HasValue ?
                new ObjectParameter("nocabang", nocabang) :
                new ObjectParameter("nocabang", typeof(int));

            var gradeParameter = grade != null ?
                new ObjectParameter("grade", grade) :
                new ObjectParameter("grade", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<mBarang>("APICabangBarangGrade", nocabangParameter, gradeParameter);
        }

        public virtual ObjectResult<transKelas> APITransKelas(Nullable<int> nosiswa, string tahunajaran)
        {
            var nosiswaParameter = nosiswa.HasValue ?
                new ObjectParameter("nosiswa", nosiswa) :
                new ObjectParameter("nosiswa", typeof(int));

            var thhnajaranParameter = tahunajaran != null ?
                new ObjectParameter("tahunajaran", tahunajaran) :
                new ObjectParameter("tahunajaran", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<transKelas>("APITransKelas", nosiswaParameter, thhnajaranParameter);
        }

      
    }
}
