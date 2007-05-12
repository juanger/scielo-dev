class CiteIndexController < ApplicationController

  def index
    render :action => 'index'
  end

  verify :method => :post, :only => [ :search ],
  :redirect_to => { :action => :index }

  def search
    @authors = Author.find(:all, :conditions => params[:record])
    @collection = [ ]
    @authors.each { |author|
      author.articles.each { |article|
        logger.info "VANCOUVER" + article.as_vancouver
        @collection.push([article.as_vancouver, article.id, article.cites_number])
      }
    }
   if @collection.size > 0
     render :action => 'results'
   else
     render :action => 'index'
   end
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
