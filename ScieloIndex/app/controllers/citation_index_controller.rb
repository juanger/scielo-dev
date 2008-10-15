class CitationIndexController < ApplicationController
  # auto_complete_for :author, :firstname
  # auto_complete_for :author, :middlename
  # auto_complete_for :author, :lastname
  auto_complete_for :article, :title
  # auto_complete_for :keyword, :name

  def index
    render :layout => 'index'
  end

  verify :method => :post, :only => [ :find_author, :find_article ], :redirect_to => { :action => :index }

  def find_basic
    
    # Finds all the articles that match any search term in:
    #  authors.firstname, authors.middlename, authors.lastname
    #  articles.title
    # The list is ordered by citations, but matching scielo articles are listed first
    
    if params[:terms].blank?
      # flash[:notice] = _('Try to set at least one parameter')
      redirect_to :action => 'index'
    end
    
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
              "LEFT OUTER JOIN associated_files as files on articles.id = files.article_id " +
              "LEFT OUTER JOIN (select article_id ,count(article_id) as citations from cites " +
              "group by article_id) as tmp ON articles.id = tmp.article_id"
    
    @collection = Article.paginate(:joins => joins,
                                   :conditions => [cond_string, cond_hash],
                                   :select => 'distinct articles.id, articles.title, '+
                                      'articles.subtitle, articles.journal_issue_id, tmp.citations, ' +
                                      'articles.fpage, articles.lpage, articles.language_id, files.filename',
                                   :count => { :select => 'DISTINCT articles.title' },
                                   :per_page => 10,
                                   :page => params[:page],
                                   :order => 'files.filename ASC NULLS LAST ,tmp.citations DESC NULLS LAST')
    
    if @collection.empty?
      flash[:notice] = _('Your search "%s" did not match any articles', params[:terms])
      redirect_to :action => 'index'
    end
  end
  
  def search
  end
  
  def find_advanced
    if params[:search].merge(params[:article]).map {|k,v| v.blank?}.inject(true) {|f,n| f && n}
      # flash[:notice] = _('Try to set at least one parameter')
      redirect_to :action => 'search'
    end
    
    session[:search] = params[:search] if params[:search]
    session[:search].merge!(params[:article])
    @collection = Search.new(session[:search] || {}, params[:page]).articles
    
    if @collection.empty?
      flash[:notice] = _('Your search did not match any articles')
      redirect_to :action => 'search'
    end
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
    @collection = Author.paginate :select => "authors.id, authors.lastname, authors.degree, authors.firstname, authors.middlename, sum(tmp.cites) as citations",
                                  :joins => "JOIN article_authors on authors.id = article_authors.author_id "+
                                            "LEFT OUTER JOIN (select article_id ,count(article_id) as cites from cites "+
                                            "group by article_id) as tmp ON article_authors.article_id = tmp.article_id ",
                                  :per_page => 30,
                                  :page => params[:page],
                                  :group => 'authors.id, authors.lastname, authors.degree, authors.firstname, authors.middlename',
                                  :order => 'citations DESC NULLS LAST',
                                  :total_entries => Author.count
  end

  def about
    respond_to do |format|
      format.html { render :template => "citation_index/about.html.erb"}
    end
  end

end
