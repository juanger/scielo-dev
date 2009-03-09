require 'pdf_writer_extensions'

class CitationIndexController < ApplicationController
  include QueryHelper

  # auto_complete_for :author, :firstname
  # auto_complete_for :author, :middlename
  # auto_complete_for :author, :lastname
  auto_complete_for :article, :title
  # auto_complete_for :journal, :title
  # auto_complete_for :keyword, :name

  def index
    render :layout => 'index'
  end

  verify :method => :post, :only => [ :find_author, :find_article ], :redirect_to => { :action => :index }


  # Finds all the articles that match any search term in:
  #  authors.firstname, authors.middlename, authors.lastname
  #  articles.title
  # The list is ordered by citations, but matching scielo articles are listed first in PostreSQL
  def find_basic
    if params[:terms].blank?
      redirect_to :action => 'index'
    end
    
    session[:search] = params[:terms] if params[:terms]
    
    @collection = Search.new(session[:search] || {}, params[:page]).articles
    
    if @collection.empty?
      flash[:notice] = _('Your search "%s" did not match any articles', params[:terms])
      redirect_to :action => 'index'
    end
  end
  
  def search
  end
  
  def find_advanced
    if params[:search] && params[:search].merge(params[:article]).map {|k,v| v.blank?}.inject(true) {|f,n| f && n}
      redirect_to :action => 'search'
    end
    
    session[:search] = params[:search] if params[:search]
    session[:search].merge!(params[:article]) if params[:article]
    @collection = Search.new(session[:search] || {}, params[:page], :advanced).articles
    
    if @collection.empty?
      flash[:notice] = _('Your search did not match any articles')
      redirect_to :action => 'search'
    end
  end
  
  def list_articles_citations
    @collection = Article.paginate :select => 'articles.*',
                                  :joins => "JOIN citations ON articles.id = citations.cited_by_article_id",
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
      format.html { render :action => 'list_authors_articles' }
      format.pdf  { render :action => 'list_authors_articles.rpdf'}
    end
  end
  
  def change_language
    session[:lang] = params[:lang].to_sym unless params[:lang].blank?
    redirect_to :back
  end

  def most_cited
    @collection = Author.paginate :select => "authors.id, authors.lastname, authors.degree, authors.firstname, authors.middlename, sum(tmp.citations) as citations",
                                  :joins => "JOIN article_authors on authors.id = article_authors.author_id "+
                                            "LEFT OUTER JOIN (select article_id ,count(article_id) as citations from citations "+
                                            "group by article_id) as tmp ON article_authors.article_id = tmp.article_id ",
                                  :per_page => 30,
                                  :page => params[:page],
                                  :group => 'authors.id, authors.lastname, authors.degree, authors.firstname, authors.middlename',
                                  :order => "citations DESC #{postgres? "NULLS LAST"}, lower(lastname) ASC",
                                  :total_entries => Author.count
  end

  def about
    respond_to do |format|
      format.html { render :template => "citation_index/about.html.erb"}
    end
  end

end
