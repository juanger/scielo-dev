class CitationIndexController < ApplicationController
  auto_complete_for :author, :firstname
  auto_complete_for :author, :middlename
  auto_complete_for :author, :lastname
  auto_complete_for :article, :title
  auto_complete_for :keyword, :name

  def index
    render :layout => 'index'
  end

  verify :method => :post, :only => [ :find_author, :find_article ], :redirect_to => { :action => :index }

  def find_basic
    terms = params[:terms].split.join("|")
    cond_string = ""
    cond_hash = {}
    joins = ""
    
    cond_hash[:terms] = terms
    
    cond_string << "authors.firstname ~* :terms "    
    cond_string << " OR " + "authors.middlename ~* :terms "
    cond_string << " OR " + "authors.lastname ~* :terms "
    cond_string << " OR " + "articles.title ~* :terms"    
    joins << " JOIN article_authors ON articles.id = article_authors.article_id " +
              "JOIN authors ON authors.id = article_authors.author_id " +
              "LEFT OUTER JOIN (select article_id ,count(article_id) as citations from cites " +
              "group by article_id) as tmp ON articles.id = tmp.article_id"
    
    @collection = Article.paginate(:joins => joins,
                                   :conditions => [cond_string, cond_hash],
                                   :select => 'distinct articles.id, articles.title, '+
                                      'articles.subtitle, articles.journal_issue_id, tmp.citations, ' +
                                      'articles.fpage, articles.lpage, articles.language_id',
                                   :count => { :select => 'DISTINCT articles.title' },
                                   :per_page => 10,
                                   :page => params[:page],
                                   :order => 'tmp.citations DESC NULLS LAST')
    
    if @collection.empty?
      flash[:notice] = _('Your search "%s" did not match any articles', params[:terms])
      redirect_to :action => 'index'
    end
  end
  
  def find_advanced
    
  end
  
  def list_articles_cites
    @collection = Article.paginate :select => 'articles.*',
                                   :joins => "JOIN cites ON articles.id = cites.cited_by_article_id",
                                   :conditions => "article_id = #{params[:id]}",
                                   :per_page => 10,
                                   :page => params[:page]
  end

  def list_authors_articles
    @author = Author.find(params[:id])
    @collection = Article.paginate :select => 'articles.*',
                                   :joins => "JOIN article_authors ON articles.id = article_authors.article_id JOIN authors ON" +
                                             " authors.id = article_authors.author_id",
                                   :conditions => ["authors.id = :id", {:id => params[:id]}],
                                   :per_page => 10,
                                   :page => params[:page]
    respond_to do |format|
      format.html { render :template => 'citation_index/list_authors_articles.html.erb' }
      format.pdf  { send_data(render(:template => 'citation_index/list_authors_articles.rfpdf', :layout => false), :type => 'application/pdf', :filename => "#{@author.lastname}_#{@author.firstname}.pdf") }
    end
  end
  
  def change_language
    session[:lang] = params[:lang].to_sym unless params[:lang].blank?
    redirect_to :back
  end

  def most_cited
    # select authors.id, sum(tmp.cites) from authors JOIN article_authors on authors.id = article_authors.author_id
    #  JOIN (select article_id ,count(article_id) as cites from cites group by article_id) as tmp ON article_authors.article_id = tmp.article_id 
    #  GROUP BY authors.id ORDER BY sum DESC LIMIT 10;
     
    @collection = Author.paginate :select => "authors.id, authors.lastname, authors.degree, authors.firstname, authors.middlename, sum(tmp.cites)",
                                  :joins => "JOIN article_authors on authors.id = article_authors.author_id "+
                                            "JOIN (select article_id ,count(article_id) as cites from cites "+
                                            "group by article_id) as tmp ON article_authors.article_id = tmp.article_id ",
                                  :per_page => 30,
                                  :page => params[:page],
                                  :group => 'authors.id, authors.lastname, authors.degree, authors.firstname, authors.middlename',
                                  :order => 'sum DESC',
                                  :total_entries => Author.count
                                            
  end

# (fold)
  #   def find_author
  #     session[:search_data] = params[:author]
  #     redirect_to :action => 'list_author_matches'
  #   end
  # 
  #   def find_article
  #     session[:search_data] = params[:article]
  #     redirect_to :action => 'search_by_article'
  #   end
  # 
  #   def find_auxiliar
  #     session[:search_data] = {:object => Author.find(params[:id]), :cites => params[:cites]}
  #     respond_to do |format|
  #       format.html { redirect_to :action => 'search_by_author' }
  #       format.pdf  { redirect_to :action => 'search_by_author', :format => 'pdf'}
  #     end
  # #    redirect_to :action => 'search_by_author'
  #   end
  # 
  #   def find_keyword
  #     session[:search_data] = params[:keyword]
  #     redirect_to :action => 'search_by_keyword'
  #   end
  #   
  #   def find_advanced
  #     content_for_session = Hash.new
  #     unless params[:author].blank?
  #       content_for_session[:author] = params[:author]
  #       author_only = true
  #     end
  #     unless params[:article].blank?
  #       content_for_session[:article] = params[:article]
  #       author_only = false
  #     end
  #     unless params[:keyword].blank?
  #       content_for_session[:keyword] = params[:keyword] 
  #       author_only = false
  #     end
  #     session[:search_data] = content_for_session
  #     if author_only
  #       redirect_to :action => 'list_author_matches'
  #     else
  #       redirect_to :action => 'search_by_advanced'
  #     end
  #   end
  # 
  #   def list_author_matches
  #     @author =  session[:search_data][:author]
  #     
  #     cond_string = ""
  #     cond_hash = {}
  #     
  #     unless first = @author[:firstname].blank?
  #       cond_string << "authors.firstname ILIKE :firstname "
  #       cond_hash[:firstname] = '%' + @author[:firstname].to_s + '%'
  #     end
  #     unless middle = @author[:middlename].blank?
  #       cond_string << (first ? "": "AND ") + "authors.middlename ILIKE :middlename "
  #       cond_hash[:middlename] = '%' + @author[:middlename].to_s + '%'
  #     end
  #     unless @author[:lastname].blank?
  #       cond_string << ((middle && first) ? "": "AND ") + "authors.lastname ILIKE :lastname "
  #       cond_hash[:lastname] = '%' + @author[:lastname].to_s + '%'
  #     end
  #     
  #     @collection = Author.paginate :conditions => [cond_string, cond_hash],
  #                                   :per_page => 10,
  #                                   :page => params[:page]
  # 
  #     if @collection.empty?
  #       flash[:notice] = "No matches found, please try another search"
  #       redirect_to :action => 'index'
  #     end
  #   end
  # 
  #   def search_by_author
  #     @author = session[:search_data][:object]
  #     @cites = session[:search_data][:cites]
  #                       
  #     @collection = Article.paginate :select => 'articles.*',
  #                                    :joins => "JOIN article_authors ON articles.id = article_authors.article_id JOIN authors ON" +
  #                                              " authors.id = article_authors.author_id",
  #                                    :conditions => ["authors.id = :id", {:id => @author.id}],
  #                                    :per_page => 10,
  #                                    :page => params[:page]
  #                  
  #     respond_to do |format|
  #       format.html { render :template => 'cite_index/search_by_author.rhtml' }
  #       format.pdf  { send_data(render(:template => 'cite_index/search_by_author.rfpdf', :layout => false), :type => 'application/pdf', :filename => "#{@author.lastname}_#{@author.firstname}.pdf") }
  #     end
  #     
  #   end
  # 
  #   def search_by_article
  #     @article = session[:search_data]
  #     @collection = Article.paginate :conditions => ["articles.title ILIKE :title", { :title => '%' + @article[:title] + '%' }],
  #                                    :per_page => 10,
  #                                    :page => params[:page]
  # 
  #     if @collection.empty?
  #       flash[:notice] = "No matches found, please try another search"
  #       redirect_to :action => 'index'
  #     end
  #   end
  # 
  #   def search_by_keyword
  #     @keyword = session[:search_data]
  #     @collection = Article.paginate :select => 'articles.*',
  #                                    :joins => "JOIN article_keywords ON articles.id = article_keywords.article_id JOIN keywords ON" +
  #                                           " keywords.id = article_keywords.keyword_id",
  #                                    :conditions => ["keywords.name ILIKE :name", {:name => '%' + @keyword[:name] + '%'}],
  #                                    :per_page => 10,
  #                                    :page => params[:page]
  # 
  #     if @collection.empty?
  #       flash[:notice] = "No matches found, please try another search"
  #       redirect_to :action => 'index'
  #     end
  #   end
  # 
  #   def search_cites
  #     @collection = Article.paginate :select => 'articles.*',
  #                                    :joins => "JOIN cites ON articles.id = cites.cited_by_article_id",
  #                                    :conditions => "article_id = #{params[:id]}",
  #                                    :per_page => 10,
  #                                    :page => params[:page]
  #   end
  #   
  #   def search_by_advanced
  #   @article = unless session[:search_data][:article].blank? then session[:search_data][:article] end
  #   @author = unless session[:search_data][:author].blank? then session[:search_data][:author] end 
  #   @keyword = unless session[:search_data][:keyword].blank? then session[:search_data][:keyword] end
  #   @busqueda = Array.new
  #   
  #   cond_string = ""
  #   cond_hash = {}
  #   joins = ""
  #   
  #   if @author
  #     unless first = @author[:firstname].blank?
  #       cond_string << "authors.firstname ILIKE :firstname "
  #       cond_hash[:firstname] = '%' + @author[:firstname].to_s + '%'
  #     end
  #     unless middle = @author[:middlename].blank?
  #       cond_string << (first ? "": " AND ") + "authors.middlename ILIKE :middlename "
  #       cond_hash[:middlename] = '%' + @author[:middlename].to_s + '%'
  #     end
  #     unless @author[:lastname].blank?
  #       cond_string << ((middle && first) ? "": " AND ") + "authors.lastname ILIKE :lastname "
  #       cond_hash[:lastname] = '%' + @author[:lastname].to_s + '%'
  #     end
  # 
  #     joins << " JOIN article_authors ON articles.id = article_authors.article_id " +
  #               "JOIN authors ON authors.id = article_authors.author_id "
  #   end
  #   
  #   if @article
  #     cond_string << (@author ? " AND " : "") + "articles.title ILIKE :title"
  #     cond_hash[:title] = "%" + @article[:title] + "%"
  #   end
  #   
  #   if @keyword
  #     cond_string << (!(@author && @article) ? "" : " AND ") + "keywords.name ILIKE :name"
  #     cond_hash[:name] = "%" + @keyword[:name] + "%"
  #     
  #     joins << "JOIN article_keywords ON articles.id = article_keywords.article_id " +
  #               "JOIN keywords ON keywords.id = article_keywords.keyword_id"
  #   end
  #   
  #   @collection = Article.paginate :joins => joins,
  #                                  :conditions => [cond_string, cond_hash],
  #                                  :per_page => 10,
  #                                  :page => params[:page]
  #   
  #   if @collection.empty?
  #     flash[:notice] = "No matches found, please try another search"
  #     redirect_to :action => 'index'
  #   end
  # end
# (end)

  

end
