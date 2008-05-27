class ScieloIndexReport < FPDF
  
   def initialize
     super
     AddPage();
     SetFont('Arial', '', 10)
   end
  
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
  
  # ISO-8859-15 convertion to UTF-8
  def Cell(w,h=0,txt='',border=0,ln=0,align='',fill=0,link='')
    @ic ||= Iconv.new('ISO-8859-15', 'UTF-8')
    # these quotation marks are not correctly rendered in the pdf
    txt = txt.gsub(/[“”]/, '"') if txt
    txt = begin
      # 0x5c char handling
      txtar = txt.split('\\')
      txtar << '' if txt[-1] == ?\\
      txtar.collect {|x| @ic.iconv(x)}.join('\\').gsub(/\\/, "\\\\\\\\")
    rescue
      txt
    end || ''
    super w,h,txt,border,ln,align,fill,link
  end
end