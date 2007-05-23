class ArticleSubjectsController < ApplicationController
  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :destroy, :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @article_subject_pages, @article_subjects = paginate :article_subjects, :per_page => 10
  end

  def show
    @article_subject = ArticleSubject.find(params[:id])
  end

  def new
    @article_subject = ArticleSubject.new
  end

  def create
    @article_subject = ArticleSubject.new(params[:article_subject])
    if @article_subject.save
      flash[:notice] = 'ArticleSubject was successfully created.'
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @article_subject = ArticleSubject.find(params[:id])
  end

  def update
    @article_subject = ArticleSubject.find(params[:id])
    if @article_subject.update_attributes(params[:article_subject])
      flash[:notice] = 'ArticleSubject was successfully updated.'
      redirect_to :action => 'show', :id => @article_subject
    else
      render :action => 'edit'
    end
  end

  def destroy
    ArticleSubject.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
