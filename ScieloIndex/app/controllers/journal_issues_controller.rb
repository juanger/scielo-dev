class JournalIssuesController < ApplicationController
  def index
    list
    render :action => 'list'
  end

  # GETs should be safe (see http://www.w3.org/2001/tag/doc/whenToUseGet.html)
  verify :method => :post, :only => [ :destroy, :create, :update ],
         :redirect_to => { :action => :list }

  def list
    @journal_issue_pages, @journal_issues = paginate :journal_issues, :per_page => 10
  end

  def show
    @journal_issue = JournalIssue.find(params[:id])
  end

  def new
    @journal_issue = JournalIssue.new
  end

  def create
    @journal_issue = JournalIssue.new(params[:journal_issue])
    if @journal_issue.save
      flash[:notice] = 'JournalIssue was successfully created.'
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def edit
    @journal_issue = JournalIssue.find(params[:id])
  end

  def update
    @journal_issue = JournalIssue.find(params[:id])
    if @journal_issue.update_attributes(params[:journal_issue])
      flash[:notice] = 'JournalIssue was successfully updated.'
      redirect_to :action => 'show', :id => @journal_issue
    else
      render :action => 'edit'
    end
  end

  def destroy
    JournalIssue.find(params[:id]).destroy
    redirect_to :action => 'list'
  end
end
