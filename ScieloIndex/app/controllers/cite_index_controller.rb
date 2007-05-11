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
end
