class CiteIndexController < ApplicationController

  def index
    session[:seach_data] = nil
    render :action => 'index'
  end

  #verify :method => :post, :only => [ :search ],
  #:redirect_to => { :action => :index }

  def search_by_author
    session[:seach_data] ||= Author.new(params[:record])
    record = session[:search_data]
    @record = params[:record]
    @pages, @collection = paginate Inflector.pluralize(Article).to_sym,
    :select => 'articles.id, articles.title, articles.journal_issue_id, articles.fpage, articles.lpage,' +
    ' articles.page_range, articles.url, articles.pacsnum, articles.other, authors.id as author_id,' +
    ' authors.firstname, authors.lastname, authors.middlename',
    :joins => 'JOIN article_authors ON articles.id = articles_authors.article_id JOIN authors ON' +
    " authors.id = articles_authors.author_id WHERE (authors.middlename = '#{record.middlename}' AND" +
    " authors.lastname = '#{record.lastname}' AND authors.firstname = '#{record.firstname}')", :per_page => 10

  end

  def search_cites
    @article = Article.find(params[:id])
    @collection = []
    @article.cites.each { |cited_by|
      @collection.push([cited_by.cite.as_vancouver])
    }
   if @collection.size > 0
     render :action => 'search_cites'
   else
     render :action => 'index'
   end
  end
end
