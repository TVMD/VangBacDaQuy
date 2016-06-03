﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BusinessLogiLayer;
namespace PresentationLayer
{
    public partial class FormThemCTNhap : Form
    {
        ChiTietMuaHangBus ct = new ChiTietMuaHangBus();
        PhieuNhap_BUS pn = new PhieuNhap_BUS();
        ChiTietPhieuNhap_BUS ctp = new ChiTietPhieuNhap_BUS();
        public FormThemCTNhap()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int kieusp = Int16.Parse(cbbKieuSP.SelectedValue.ToString());
            int loaisp = Int16.Parse(cbbLoaiSP.SelectedValue.ToString());
            int sopn = Int16.Parse(cbbSoPhieuNhap.Text);
            if (txtDonGia.Text == "" || txtSoLuong.Text == "" )
            {
                MessageBox.Show("Bạn còn dữ liệu chưa nhập");
                return;
            }
            int masp = 0;
            if (ct.KiemTraSP(kieusp, loaisp) == -1)
                MessageBox.Show("chua co sp");
            else masp = ct.KiemTraSP(kieusp, loaisp);
            CTPhieuNhap_DTO a = new CTPhieuNhap_DTO();
            a.SoPhieuNhap = sopn;
            a.SLNhap = Int16.Parse(txtSoLuong.Text);
            a.DonGia = Decimal.Parse(txtDonGia.Text);
            a.ThanhTien = a.DonGia * a.SLNhap;
            a.MaSP = masp;
            ctp.ThemChiTietNhap(a);
        }

        private void FormThemCTNhap_Load(object sender, EventArgs e)
        {
            cbbKieuSP.DataSource = ct.LayKieuSP();
            cbbKieuSP.DisplayMember = "TenKieuSP";
            cbbKieuSP.ValueMember = "MaKieuSP";
            cbbLoaiSP.DataSource = ct.LayLoaiSP();
            cbbLoaiSP.DisplayMember = "TenLoaiSP";
            cbbLoaiSP.ValueMember = "MaLoaiSP";
            cbbSoPhieuNhap.DataSource = pn.LayTatCa();
            cbbSoPhieuNhap.DisplayMember = "SoPhieuNhap";
            cbbSoPhieuNhap.ValueMember = "SoPhieuNhap";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormThemCTNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
