class CiteIndexController < ApplicationController

  def index
    render :action => 'index'
  end
 
  verify :method => :post, :only => [ :search ],
         :redirect_to => { :action => :index }

  def search
    @authors = Author.find(:all, :conditions => params[:record])
    @articles = [ ]
    @authors.each { |author|
	author.articles.each { |article|
 		@articles.push(article)
	}	
     }
   if @articles > 0
    render :action => 'results'
   else
    render :action => 'index'
   end
  end	
end
