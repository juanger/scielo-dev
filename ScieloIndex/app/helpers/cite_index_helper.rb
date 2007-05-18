module CiteIndexHelper
  def pdf_download_link(article_id)
    article = Article.find(article_id)
    link_to "Download PDF", :controller => 'associated_files', :action => 'send_file', :id => article.associated_file, :format => 'PDF'
  end

  def html_view_link(article_id)
    article = Article.find(article_id)
    link_to "View HTML", :controller => 'associated_files', :action => 'send_file', :id => article.associated_file, :format => 'HTML'
  end
end
