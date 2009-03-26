class AssociatedFiles

  def initialize(hash = {})
    @path = hash[:path]
    @journal = hash[:journal]
    @issue = hash[:issue]
    @article = hash[:article]        
    
    @article_id = hash[:id]
    @logger = hash[:logger]
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
    
    files.sgml_path = File.join(issue_path, "markup", "#{@article}.txt")
    
    if files.save
    else
      @logger.error_message("Associated file doesn't exist")
      files.errors.each do |key, value|
        @logger.error("Artículo #{@article} de la revista #{@journal}", "#{key}: #{value}")
      end
      @logger.error(filename, files.errors)
    end
  end
end