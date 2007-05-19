class CiteIndexController < ApplicationController

  def index
    render :action => 'index'
  end

  #verify :method => :post, :only => [ :search ],
  #:redirect_to => { :action => :index }

  def search
    @record = params[:record]
    @pages, @collection = paginate Inflector.pluralize(Article).to_sym, :per_page => 10, :joins =>
      "JOIN article_authors ON articles.id = article_authors.author_id JOIN authors ON authors.id = article_authors.author_id " +
      "WHERE (authors.middlename = '#{params[:record][:middlename]}' AND authors.lastname = '#{params[:record][:lastname]}' AND authors.firstname = '#{params[:record][:firstname]}')",
    :select => "authors.id, authors.firstname, authors.middlename, authors.lastname, " +
      "articles.id, articles.title, articles.journal_issue_id, articles.fpage, articles.lpage, " +
      "articles.page_range, articles.url, articles.pacsnum, articles.other"
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
