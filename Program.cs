// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;

namespace ManajemenKaryawan
{
    // Kelas abstrak Karyawan (base class)
    public abstract class Karyawan
    {
        public string Nama { get; set; }
        public double Gaji { get; set; }

        public Karyawan(string nama, double gaji)
        {
            Nama = nama;
            Gaji = gaji;
        }

        // Method virtual yang akan di-override
        public virtual void Kerja()
        {
            Console.WriteLine($"{Nama} sedang bekerja sebagai karyawan umum.");
        }

        public virtual void InfoKaryawan()
        {
            Console.WriteLine($"Nama: {Nama}, Gaji: Rp {Gaji:N0}");
        }
    }

    // Kelas Tetap mewarisi Karyawan
    public class Tetap : Karyawan
    {
        public double Tunjangan { get; set; }

        public Tetap(string nama, double gaji, double tunjangan) : base(nama, gaji)
        {
            Tunjangan = tunjangan;
        }

        public double HitungGajiTotal()
        {
            return Gaji + Tunjangan;
        }

        public override void InfoKaryawan()
        {
            Console.WriteLine($"Tetap - Nama: {Nama}, Gaji: Rp {Gaji:N0}, Tunjangan: Rp {Tunjangan:N0}, Total: Rp {HitungGajiTotal():N0}");
        }
    }

    // Kelas Kontrak mewarisi Karyawan
    public class Kontrak : Karyawan
    {
        public int Durasi { get; set; } // dalam bulan

        public Kontrak(string nama, double gaji, int durasi) : base(nama, gaji)
        {
            Durasi = durasi;
        }

        public bool CekKontrak()
        {
            return Durasi > 0;
        }

        public override void InfoKaryawan()
        {
            Console.WriteLine($"Kontrak - Nama: {Nama}, Gaji: Rp {Gaji:N0}, Durasi: {Durasi} bulan, Aktif: {CekKontrak()}");
        }
    }

    // Manager mewarisi Tetap
    public class Manager : Tetap
    {
        public Manager(string nama, double gaji, double tunjangan) : base(nama, gaji, tunjangan)
        {
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} sedang memimpin tim sebagai Manager.");
        }

        public void Memimpin()
        {
            Console.WriteLine($"{Nama} sedang memimpin rapat strategis.");
        }
    }

    // Staff mewarisi Tetap
    public class Staff : Tetap
    {
        public Staff(string nama, double gaji, double tunjangan) : base(nama, gaji, tunjangan)
        {
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} sedang mengerjakan tugas harian sebagai Staff.");
        }

        public void KerjakanTugas()
        {
            Console.WriteLine($"{Nama} sedang menyelesaikan laporan bulanan.");
        }
    }

    // Magang mewarisi Kontrak
    public class Magang : Kontrak
    {
        public Magang(string nama, double gaji, int durasi) : base(nama, gaji, durasi)
        {
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} sedang belajar sebagai Magang.");
        }

        public void Belajar()
        {
            Console.WriteLine($"{Nama} sedang mengikuti pelatihan onboarding.");
        }
    }

    // Freelancer mewarisi Kontrak
    public class Freelancer : Kontrak
    {
        public Freelancer(string nama, double gaji, int durasi) : base(nama, gaji, durasi)
        {
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} sedang mengambil proyek freelance.");
        }

        public void AmbilProyek()
        {
            Console.WriteLine($"{Nama} sedang mengerjakan proyek web development.");
        }
    }

    // Kelas Perusahaan
    public class Perusahaan
    {
        private List<Karyawan> daftarKaryawan = new List<Karyawan>();

        public void TambahKaryawan(Karyawan karyawan)
        {
            daftarKaryawan.Add(karyawan);
            Console.WriteLine($"✓ {karyawan.Nama} berhasil ditambahkan ke perusahaan.");
        }

        public void DaftarKaryawan()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("DAFTAR SELURUH KARYAWAN");
            Console.WriteLine(new string('=', 60));

            foreach (Karyawan karyawan in daftarKaryawan)
            {
                karyawan.InfoKaryawan();
                Console.WriteLine();
            }
        }

        // Demonstrasi Polymorphism
        public void SemuaKerja()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("SEMUA KARYAWAN SEDANG BEKERJA");
            Console.WriteLine(new string('=', 60));

            foreach (Karyawan karyawan in daftarKaryawan)
            {
                karyawan.Kerja();
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== MANAJEMEN KARYAWAN ===");
            Console.WriteLine();

            // a. Buat objek perusahaan
            Perusahaan ptABC = new Perusahaan();

            // b. Buat beberapa objek karyawan
            Manager manager1 = new Manager("Ikhsan Alim Farabi", 15000000, 5000000);
            Staff staff1 = new Staff("Febrian", 8000000, 1500000);
            Magang magang1 = new Magang("Reyhan", 2000000, 6);
            Freelancer freelancer1 = new Freelancer("Excel", 12000000, 3);

            // c. Tambahkan ke perusahaan
            ptABC.TambahKaryawan(manager1);
            ptABC.TambahKaryawan(staff1);
            ptABC.TambahKaryawan(magang1);
            ptABC.TambahKaryawan(freelancer1);

            // d. Tampilkan semua data
            ptABC.DaftarKaryawan();

            // e. Demonstrasikan polymorphism
            ptABC.SemuaKerja();

            // f. Panggil method khusus
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("DEMONSTRASI METHOD KHUSUS");
            Console.WriteLine(new string('=', 60));

            // Method khusus Manager
            manager1.Memimpin();

            // Method khusus Staff
            staff1.KerjakanTugas();

            // Method khusus Magang
            magang1.Belajar();

            // Method khusus Freelancer
            freelancer1.AmbilProyek();

            // Test method tambahan
            Console.WriteLine($"\nGaji Total Manager {manager1.Nama}: Rp {manager1.HitungGajiTotal():N0}");
            Console.WriteLine($"Status Kontrak {freelancer1.Nama}: {freelancer1.CekKontrak()}");

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("Program selesai!");
            Console.ReadKey();
        }
    }
}