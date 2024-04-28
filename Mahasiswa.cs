using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApiMahasiswa
{
    public class Mahasiswa
    {
        public string Nama { get; set; }
        public string Nim { get; set; }
    }

    public class MahasiswaController : ControllerBase
    {
        private static List<Mahasiswa> mahasiswaData = new List<Mahasiswa>()
        {
            new Mahasiswa { Nama = "LeBron James", Nim = "1302000001" },
            new Mahasiswa { Nama = "Stephen Curry", Nim = "1302000002" },
            // Tambahkan data anggota kelompok lain di sini
        };

        [HttpGet("/api/mahasiswa")]
        public IActionResult GetAllMahasiswa()
        {
            return Ok(mahasiswaData);
        }

        [HttpGet("/api/mahasiswa/{index}")]
        public IActionResult GetMahasiswaByIndex(int index)
        {
            if (index < 0 || index >= mahasiswaData.Count)
            {
                return NotFound("Index tidak valid");
            }
            return Ok(mahasiswaData[index]);
        }

        [HttpPost("/api/mahasiswa")]
        public IActionResult AddMahasiswa([FromBody] Mahasiswa newMahasiswa)
        {
            if (newMahasiswa == null || string.IsNullOrEmpty(newMahasiswa.Nama) || string.IsNullOrEmpty(newMahasiswa.Nim))
            {
                return BadRequest("Nama dan NIM harus diisi");
            }

            mahasiswaData.Add(newMahasiswa);
            return CreatedAtRoute("GetMahasiswaByIndex", new { index = mahasiswaData.Count - 1 }, newMahasiswa);
        }

        [HttpDelete("/api/mahasiswa/{index}")]
        public IActionResult DeleteMahasiswaByIndex(int index)
        {
            if (index < 0 || index >= mahasiswaData.Count)
            {
                return NotFound("Index tidak valid");
            }

            mahasiswaData.RemoveAt(index);
            return Ok("Mahasiswa dihapus");
        }
    }
}

