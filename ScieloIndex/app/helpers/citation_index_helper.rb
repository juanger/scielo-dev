module CitationIndexHelper
  def pdf_download_link(article)
    if article.associated_file
      link_to image_tag("dl_pdf.png", :width => '32'), :controller => 'associated_files', :action => 'send_file', :id => article.associated_file, :format => 'pdf'
    else
      nil
    end
  end

  def html_view_link(article)
    if article.associated_file
      link_to image_tag("view_html.png", :width => '32'), :controller => 'associated_files', :action => 'send_file', :id => article.associated_file, :format => 'html'
    else
      nil
    end
  end

  def hidden_div_if(condition, attributes = {}, &block)
    if condition
      attributes["style"] = "display: none"
    end
    content_tag("div", attributes, &block)
  end
end
