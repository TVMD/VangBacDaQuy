﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System;
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
    public partial class M_PhieuNoEdit : Form
    {
        PhieuMuaHang_DTO PhieuNo = new PhieuMuaHang_DTO();
        M_PhieuNoBLL PhieuNoBLL = new M_PhieuNoBLL();
        M_PhieuBanHangBLL PhieuBanBLL = new M_PhieuBanHangBLL();
        int Edit = 0; // 0- them chay . 1- them tu phieu ban 2-update
        public M_PhieuNoEdit() // them chay
        {
            Edit = 0;
            InitializeComponent();

            comboBoxPhieuBan.DataSource = PhieuBanBLL.SelectTop(0);
            comboBoxPhieuBan.ValueMember = "SoPhieuBan";
            comboBoxPhieuBan.DisplayMember = "SoPhieuBan";

            txtSoPhieuNo.Text = PhieuNoBLL.GetSoPhieuNo().ToString();
            txtSoPhieuNo.Enabled = false;
        }

        public M_PhieuNoEdit(int sophieuban) // them tu phieu ban
        {
            Edit = 1;
            InitializeComponent();

            comboBoxPhieuBan.Text = sophieuban.ToString();
            comboBoxPhieuBan.Enabled = false;
            txtSoPhieuNo.Text = PhieuNoBLL.GetSoPhieuNo().ToString();
            txtSoPhieuNo.Enabled = false;
            
            var x = PhieuBanBLL.SelectTop(sophieuban, 0).FirstOrDefault();
            txtSoTienTra.Text = x.SoTienTra.ToString();
            txtConLai.Text = (x.TongTien - x.SoTienTra).ToString();

        }

        public M_PhieuNoEdit(PhieuNo_DTO p) // update 
        {
            Edit = 2;
            InitializeComponent();

            txtSoPhieuNo.Text = p.SoPhieuNo.ToString();
            txtSoPhieuNo.Enabled = false;
            comboBoxPhieuBan.Text = p.SoPhieuBan.ToString();
            comboBoxPhieuBan.Enabled = false;

            dateTimePickerNgayNo.Text = p.NgayNo;
            dateTimePickerNgayThanhToan.Text = p.NgayThanhToan;
            txtSoTienTra.Text = p.SoTienTra.ToString();
            txtConLai.Text = p.SoTienConLai.ToString();

        }

        private void M_PhieuNoEdit_Load(object sender, EventArgs e)
        {

        }

        private void toolStripLuu_Click_1(object sender, System.EventArgs e)
        {
            var pbh = PhieuBanBLL.SelectTop(int.Parse(comboBoxPhieuBan.Text), 0).FirstOrDefault();
            PhieuNo_DTO p = new PhieuNo_DTO()
            {
                SoPhieuNo = int.Parse(txtSoPhieuNo.Text),
                SoPhieuBan = int.Parse(comboBoxPhieuBan.Text),
                NgayNo = dateTimePickerNgayNo.Value.ToShortDateString(),
                NgayThanhToan = dateTimePickerNgayThanhToan.Value.ToShortDateString(),
                SoTienTra = txtSoTienTra.Text == "" ? 0 : decimal.Parse(txtSoTienTra.Text),
                SoTienConLai = txtConLai.Text == "" ? 0 : decimal.Parse(txtConLai.Text)
            };

            if (Edit == 0)
            {
                if (pbh.SoTienTra == pbh.TongTien)
                {
                    MessageBox.Show("Phiếu bán này đã được trả đủ.");
                    return;
                }
                PhieuNoBLL.Insert(p);
            }
            if (Edit == 1)
            {
                PhieuNoBLL.Insert(p);
            }
            if (Edit == 2)
            {
                PhieuNoBLL.Update(p);
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

    }
}
