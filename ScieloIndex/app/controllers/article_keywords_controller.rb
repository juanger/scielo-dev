class ArticleKeywordsController < ApplicationController
  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :destroy, :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @article_keyword_pages, @article_keywords = paginate :article_keywords, :per_page => 10
  end

  def show
    @article_keyword = ArticleKeyword.find(params[:id])
  end

  def new
    @article_keyword = ArticleKeyword.new
  end

  def create
    @article_keyword = ArticleKeyword.new(params[:article_keyword])
    if @article_keyword.save
      flash[:notice] = 'ArticleKeyword was successfully created.'
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @article_keyword = ArticleKeyword.find(params[:id])
  end

  def update
    @article_keyword = ArticleKeyword.find(params[:id])
    if @article_keyword.update_attributes(params[:article_keyword])
      flash[:notice] = 'ArticleKeyword was successfully updated.'
      redirect_to :action => 'show', :id => @article_keyword
    else
      render :action => 'edit'
    end
  end

  def destroy
    ArticleKeyword.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
