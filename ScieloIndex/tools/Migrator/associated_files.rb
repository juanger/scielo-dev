class AssociatedFiles

  def initialize(hash = {})
    @path = hash[:path]
    @journal = hash[:journal]
    @issue = hash[:issue]
    @article = hash[:article]        
    
    @article_id = hash[:id]
    create_associated_files()
  end
  
  def create_associated_files
    files = AssociatedFile.new
    files.article_id = @article_id
    files.filename = @article
    issue_path = File.join(@path, @journal, @issue)
    
    pdf = File.join(issue_path, "pdf", "#{@article}.pdf")
    files.pdf_path = pdf if File.file? pdf
    
    html = File.join(issue_path, "body", "#{@article}.htm")
    files.html_path = html if File.file? html
    
    if files.save
    else
    end
  end
end