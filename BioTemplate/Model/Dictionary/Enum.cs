using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BioTemplate.Model.Dictionary
{

    public enum Bulan : byte
    {
        Januari = 1, Februari, Maret, April, Mei, Juni, Juli, Agustus, September, Oktober, November, Desember
    }

    public enum BulanRomawi : byte
    {
        I = 1, II, III, IV, V, VI, VII, VIII, IX, X, XI, XII
    }

    public enum PayrollComponent : byte
    {
        Pendapatan = 1, Potongan, RapelPendapatan, RapelPotongan
    }

    public enum StatusDocument : byte
    {
        Draft = 0,
        Dikirim,
        Disetujui,
        Dikoreksi,
        Ditolak
    }

}