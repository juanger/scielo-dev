class ArticleAuthorsController < ApplicationController
  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :destroy, :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @article_author_pages, @article_authors = paginate :article_authors, :per_page => 10
  end

  def show
    @article_author = ArticleAuthor.find(params[:id])
  end

  def new
    @article_author = ArticleAuthor.new
  end

  def create
    @article_author = ArticleAuthor.new(params[:article_author])
    if @article_author.save
      flash[:notice] = 'ArticleAuthor was successfully created.'
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @article_author = ArticleAuthor.find(params[:id])
  end

  def update
    @article_author = ArticleAuthor.find(params[:id])
    if @article_author.update_attributes(params[:article_author])
      flash[:notice] = 'ArticleAuthor was successfully updated.'
      redirect_to :action => 'show', :id => @article_author
    else
      render :action => 'edit'
    end
  end

  def destroy
    ArticleAuthor.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
