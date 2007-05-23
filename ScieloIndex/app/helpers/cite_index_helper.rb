module CiteIndexHelper
  def pdf_download_link(article)
    if article.associated_file
      link_to "Download PDF", :controller => 'associated_files', :action => 'send_file', :id => article.associated_file, :format => 'PDF'
    else
      nil
    end
  end

  def html_view_link(article)
    if article.associated_file
      link_to "View HTML", :controller => 'associated_files', :action => 'send_file', :id => article.associated_file, :format => 'HTML'
    else
      nil
    end
  end
end
