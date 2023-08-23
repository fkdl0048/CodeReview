namespace Chapter04;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label titleLabel = null;
    private System.Windows.Forms.Label header_1 = null;
    private System.Windows.Forms.Label header_2 = null;
    private System.Windows.Forms.Label header_3 = null;

    private Font titleFont = null;
    private Font bodyFont = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        titleFont = new Font("Arial", 16, FontStyle.Bold);
        bodyFont = new Font("Arial", 10, FontStyle.Regular);

        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(600, 350);
        this.Text = "리팩터링 Chapter04";

        this.titleLabel = new System.Windows.Forms.Label();
        this.titleLabel.Location = new System.Drawing.Point(10, 10);
        this.titleLabel.Size = new System.Drawing.Size(500, 30);
        this.titleLabel.Text = "지역:" + province.GetName();
        this.titleLabel.Font = titleFont;
        this.Controls.Add(this.titleLabel);

        this.header_1 = new System.Windows.Forms.Label();
        this.header_1.Location = new System.Drawing.Point(30, 60);
        this.header_1.Size = new System.Drawing.Size(200, 20);
        this.header_1.Text = $"수요: {province.GetDemand()}     가격: {province.GetPrice()}";
        this.header_1.Font = bodyFont;
        this.Controls.Add(this.header_1);

        this.header_2 = new System.Windows.Forms.Label();
        this.header_2.Location = new System.Drawing.Point(10, 120);
        this.header_2.Size = new System.Drawing.Size(100, 20);
        this.header_2.Text = $"생산자 수: {province.GetProducers().Count}";
        this.header_2.Font = bodyFont;
        this.Controls.Add(this.header_2);

        // FlowLayoutPanel 생성
        FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
        flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        flowLayoutPanel.Location = new System.Drawing.Point(30, 150);
        flowLayoutPanel.Size = new System.Drawing.Size(1000, 150);

        // FlowLayoutPanel에 Button 컨트롤 추가
        for (int i = 0; i < province.GetProducers().Count ; i++)
        {
            Label label = new Label();
            label.Text = $"{province.GetProducers()[i].GetName()}:  비용: {province.GetProducers()[i].GetCost()}  생산량: {province.GetProducers()[i].GetProduction()} 수익: {province.GetProducers()[i].GetCost() * province.GetProducers()[i].GetProduction()}";
            label.Font = bodyFont;
            label.Size = new System.Drawing.Size(300, 40);
            flowLayoutPanel.Controls.Add(label);
        }

        this.Controls.Add(flowLayoutPanel); // 폼에 FlowLayoutPanel 추가

        this.header_3 = new System.Windows.Forms.Label();
        this.header_3.Location = new System.Drawing.Point(10, 300);
        this.header_3.Size = new System.Drawing.Size(200, 20);
        this.header_3.Text = $"부족분: {province.Shortfall()} 총수익: {province.Profit()}";
        this.header_3.Font = bodyFont;
        this.Controls.Add(this.header_3);

        this.ResumeLayout(false);
    }

    #endregion
}
