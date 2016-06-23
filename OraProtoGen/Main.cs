﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.IO;

namespace OraProtoGen
{
    public enum GenFmt
    {
        Proto,
        Cs
    }

    public partial class Main : Form
    {
        private GenFmt _lastFmt;

        public Main()
        {
            InitializeComponent();
        }

        private string fixName(string v)
        {
            return v.Replace("$", "_").ToLower();
        }

        private string ora2proto(string t, decimal len, decimal acc)
        {
            string ret = "ERROR";

            switch (t.ToLower())
            {
                case "nchar":
                case "nvarchar2":
                case "varchar2":
                case "char":
                    {
                        ret = "string";
                        break;
                    }
                case "float":
                    {
                        ret = "float";
                        break;
                    }
                case "real":
                case "smallint":
                case "int":
                case "integer":
                    {
                        ret = "int32";
                        break;
                    }
                case "number":
                    {
                        ret = "double";
                        break;
                    }
                case "date":
                    {
                        ret = "uint64";
                        break;
                    }
                default:
                    {
                        ret = "??" + t + "??";
                        break;
                    }
            }

            return ret;
        }

        private string ora2cs(string t, decimal len, decimal acc)
        {
            string ret = "ERROR";

            switch (t.ToLower())
            {
                case "nchar":
                case "nvarchar2":
                case "varchar2":
                case "char":
                    {
                        ret = "string";
                        break;
                    }
                case "float":
                case "real":
                case "smallint":
                case "int":
                case "integer":
                case "number":
                    {
                        ret = "decimal";
                        break;
                    }
                case "date":
                    {
                        ret = "DateTime";
                        break;
                    }
                default:
                    {
                        ret = "??" + t + "??";
                        break;
                    }
            }

            return ret;
        }

        private string genProto(List<ColSpec> cs)
        {
            string nl = Environment.NewLine;

            StringBuilder b = new StringBuilder();
            b.Append(string.Format("message {0} {{", cs.First().table_name.ToLower()));

            var x = 1;
            foreach (var r in cs)
            {
                //ignore columns with $ or # in the name
                if (!r.column_name.Contains('#') && !r.column_name.Contains('$'))
                {
                    b.Append(string.Format("{0}{0}\t// {1}{0}\t{2} {3} {4} = {5};",
                        nl,
                        r.comments,
                        (r.nullable == "Y" ? "required" : "optional"),
                        ora2proto(r.data_type, r.data_length, r.data_precision),
                        fixName(r.column_name),
                        x
                        ));
                    x++;
                }
            }

            b.Append(Environment.NewLine + "};");

            return b.ToString();
        }

        private string genCs(List<ColSpec> cs)
        {
            string nl = Environment.NewLine;

            StringBuilder b = new StringBuilder();
            b.Append(string.Format("//THIS FILE WAS AUTOGENERATED FROM {1} SCHEMA{0}//DO NOT EDIT THIS FILE{0}//GENERATED {2}{0}{0}#region {3}{0}", nl, cs.First().owner, DateTime.Now.ToString("dd/MM/yyyy HH:mm"), cs.First().table_name));
            b.Append("using System;" + nl);
            b.Append("using ProtoBuf;" + nl + nl);

            b.Append(string.Format("namespace OraProtoGen.{0} {{{1}", cs.First().owner, nl));
            b.Append(string.Format("\t[ProtoContract]{0}\tpublic class {1} {{", nl, cs.First().table_name.ToLower()));

            var x = 1;
            foreach (var r in cs)
            {
                //ignore columns with $ or # in the name
                if (!r.column_name.Contains('#') && !r.column_name.Contains('$'))
                {
                    b.Append(string.Format("{0}\t\t/// <summary>{0}\t\t/// {1}{0}\t\t/// </summary>{0}\t\t[ProtoMember({2})]{0}\t\tpublic {3} {4} {{ get; set; }}{0}",
                        nl,
                        r.comments,
                        (r.nullable == "Y" ? x.ToString() : string.Format("{0}, Options = MemberSerializationOptions.Required", x)),
                        ora2cs(r.data_type, r.data_length, r.data_precision),
                        fixName(r.column_name)
                        ));
                    x++;
                }
            }

            b.Append(string.Format("{0}\t}}{0}}}{0}#endregion", nl));

            return b.ToString();
        }

        private void genInit(GenFmt fmt)
        {
            bool allTables = string.IsNullOrEmpty(tableInput.Text);
            string outdir = string.Empty;

            if (allTables)
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    outdir = dlg.SelectedPath;
                }
            }

            _lastFmt = fmt;
            try
            {
                using (OracleConnection conn = new OracleConnection(connstringInput.Text))
                {
                    conn.Open();

                    string qry = @"select 
  dba_tab_cols.owner, 
  dba_tab_cols.table_name, 
  dba_tab_cols.column_name, 
  dba_tab_cols.data_type, 
  dba_tab_cols.data_length, 
  dba_tab_cols.nullable, 
  all_col_comments.comments 
from 
  dba_tab_cols, all_col_comments 
where 
  all_col_comments.table_name = dba_tab_cols.table_name and 
  all_col_comments.owner = dba_tab_cols.owner and 
  all_col_comments.column_name = dba_tab_cols.column_name and
  dba_tab_cols.owner = :o and 
  dba_tab_cols.table_name = :t 
order by dba_tab_cols.internal_column_id";

                    if (allTables)
                    {
                        foreach (string t in getTableNames())
                        {
                            var res = conn.Query<ColSpec>(qry, new { o = schemaInput.Text, t = t });

                            string fout = fmt == GenFmt.Proto ? genProto(res.ToList()) : genCs(res.ToList());
                            File.WriteAllText(string.Format("{0}\\{1}.{2}", outdir, t, fmt == GenFmt.Proto ? "proto" : "cs"), fout);
                        }
                    }
                    else
                    {
                        var res = conn.Query<ColSpec>(qry, new { o = schemaInput.Text, t = tableInput.Text });

                        outTxt.Text = fmt == GenFmt.Proto ? genProto(res.ToList()) : genCs(res.ToList());
                        outTxt.Enabled = true;
                    }


                    conn.Close();
                    OracleConnection.ClearPool(conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private List<string> getTableNames()
        {
            List<string> ret = new List<string>();

            try
            {
                using (OracleConnection conn = new OracleConnection(connstringInput.Text))
                {
                    conn.Open();

                    ret = conn.Query<string>("select distinct(table_name) from dba_tab_cols where owner = :o order by table_name", new { o = schemaInput.Text }).ToList();

                    conn.Close();
                    OracleConnection.ClearPool(conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try to get schemas and table names
            try
            {
                using (OracleConnection conn = new OracleConnection(connstringInput.Text))
                {
                    conn.Open();

                    var res = conn.Query<string>("select distinct(owner) from dba_tab_cols order by owner");
                    schemaInput.Items.Clear();
                    schemaInput.Items.AddRange(res.ToArray());

                    schemaInput.Enabled = true;

                    conn.Close();
                    OracleConnection.ClearPool(conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableInput.Items.Clear();
            tableInput.Items.AddRange(getTableNames().ToArray());

            tableInput.Enabled = true;
            protoBtn.Enabled = true;
            csBtn.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            genInit(GenFmt.Proto);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = _lastFmt == GenFmt.Proto ? "PROTO|*.proto|All|*.*" : "CS|*.cs|All|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlg.FileName, outTxt.Text);
            }
        }

        private void csBtn_Click(object sender, EventArgs e)
        {
            genInit(GenFmt.Cs);
        }
    }

    internal class ColSpec
    {
        public string owner { get; set; }
        public string table_name { get; set; }
        public string column_name { get; set; }
        public string data_type { get; set; }
        public decimal data_length { get; set; }
        public decimal data_precision { get; set; }
        public string nullable { get; set; }
        public string comments { get; set; }
    }
}