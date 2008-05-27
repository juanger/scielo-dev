class ScieloIndexReport < FPDF
  
  # def initialize
  #   AddPage();
  #   SetFont('Arial', '', 10)
  # end
  
  def Header
    self.Image("#{File.join(RAILS_ROOT, 'public')}/images/logo_small.png", 10, 10, 0, 20) 
    self.Image("#{File.join(RAILS_ROOT, 'public')}/images/logo_unam_noalpha.png", 160, 13, 0, 15) 
    self.Image("#{File.join(RAILS_ROOT, 'public')}/images/logo_dgb_noalpha.png", 175, 13, 0, 15)
    self.Ln(30)
  end
  
  def Footer

  end
  
  def Title(author_name)
    SetFont('Arial', '', 20)
    Cell(0,0,"Citation List",0,0,"C")
    Ln(10)
    Cell(0,0,"#{author_name}",0,0,"C")
    Ln(20)
  end
  
end