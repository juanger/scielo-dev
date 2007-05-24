class CiteIndexController < ApplicationController
  auto_complete_for :author, :firstname
  auto_complete_for :author, :middlename
  auto_complete_for :author, :lastname
  auto_complete_for :article, :title

  def index
    session[:search_data] = nil
    if session[:form]
      @form = session[:form]
    end
    render :action => 'index'
  end

  def show_form
    @form = session[:form] = params[:form]
  end

  verify :method => :post, :only => [ :find_author, :find_article ], :redirect_to => { :action => :index }

  def find_author
    session[:search_data] = Author.find(:first, :conditions => params[:author])
    if session[:search_data].nil?
      flash[:notice] = "Author not found, please try another search"
      redirect_to :action => 'index'
    else
      redirect_to :action => 'search_by_author'
    end
  end

  def find_article
    session[:search_data] = Article.find(:first, :conditions => params[:article])
    if session[:search_data].nil?
      flash[:notice] = "Article not found, please try another search"
      redirect_to :action => 'index'
    else
      redirect_to :action => 'search_by_title'
    end
  end

  def search_by_author
    @author = session[:search_data]
    @pages, @collection = paginate Inflector.pluralize(Article).to_sym,
    :select => 'articles.id, articles.title, articles.journal_issue_id, articles.fpage, articles.lpage,' +
    ' articles.page_range, articles.url, articles.pacsnum, articles.other, authors.id as author_id,' +
    ' authors.firstname, authors.lastname, authors.middlename',
    :joins => "JOIN article_authors ON articles.id = article_authors.article_id JOIN authors ON" +
    " authors.id = article_authors.author_id WHERE (authors.middlename = '#{@author.middlename}' AND" +
    " authors.lastname = '#{@author.lastname}' AND authors.firstname = '#{@author.firstname}')", :per_page => 10
  end

  def search_by_title
    @article = session[:search_data]
    @pages, @collection = paginate Inflector.pluralize(Article).to_sym,
    :conditions => "articles.id = #{@article.id}",
    :per_page => 10
  end

  def search_cites
    @pages, @collection = paginate Inflector.pluralize(Article).to_sym,
    :select => 'articles.id, articles.title, articles.journal_issue_id, articles.fpage, articles.lpage,' +
      ' articles.page_range, articles.url, articles.pacsnum, articles.other',
    :joins => "JOIN cites ON articles.id = cites.cited_by_article_id",
    :conditions => "article_id = #{params[:id]}",
    :per_page => 10
  end
end
