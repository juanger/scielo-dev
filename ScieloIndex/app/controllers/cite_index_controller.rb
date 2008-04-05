class CiteIndexController < ApplicationController
  auto_complete_for :author, :firstname
  auto_complete_for :author, :middlename
  auto_complete_for :author, :lastname
  auto_complete_for :article, :title
  auto_complete_for :keyword, :name

  def index
    session[:search_data] = nil
    session[:form_fields_in] = Array.new
    if session[:form]
      @form = session[:form]
    end
    render :action => 'index'
  end

  def show_form
    @form = session[:form] = params[:form]
  end
  
  def show_form_advanced
    @form_advanced_fields = params[:form_advanced].split(':')
  end

  verify :method => :post, :only => [ :find_author, :find_article ], :redirect_to => { :action => :index }

  def find_author
    session[:search_data] = params[:author]
    redirect_to :action => 'list_author_matches'
  end

  def find_article
    session[:search_data] = params[:article]
    redirect_to :action => 'search_by_article'
  end

  def find_auxiliar
    session[:search_data] = {:object => Author.find(params[:id]), :cites => params[:cites]}
    redirect_to :action => 'search_by_author'
  end

  def find_keyword
    session[:search_data] = params[:keyword]
    redirect_to :action => 'search_by_keyword'
  end
  
  def find_advanced
    content_for_session = Hash.new
    unless params[:author].blank?
    	content_for_session[:author] = params[:author]
    	author_only = true
    end
    unless params[:article].blank?
    	content_for_session[:article] = params[:article]
    	author_only = false
    end
    unless params[:keyword].blank?
      content_for_session[:keyword] = params[:keyword] 
      author_only = false
    end
    session[:search_data] = content_for_session
    if author_only
      redirect_to :action => 'list_author_matches'
    else
      redirect_to :action => 'search_by_advanced'
    end
  end

  def list_author_matches
    @author =  session[:search_data][:author]
    
    cond_string = ""
    cond_hash = {}
    
    unless first = @author[:firstname].blank?
      cond_string << "authors.firstname ILIKE :firstname "
      cond_hash[:firstname] = '%' + @author[:firstname].to_s + '%'
    end
    unless middle = @author[:middlename].blank?
      cond_string << (first ? "": "AND ") + "authors.middlename ILIKE :middlename "
      cond_hash[:middlename] = '%' + @author[:middlename].to_s + '%'
    end
    unless @author[:lastname].blank?
      cond_string << ((middle && first) ? "": "AND ") + "authors.lastname ILIKE :lastname "
      cond_hash[:lastname] = '%' + @author[:lastname].to_s + '%'
    end
         
    @pages, @collection = paginate Inflector.pluralize(Author.to_s).to_sym,
    :conditions => [cond_string, cond_hash],
    :per_page => 10

    if @collection.empty?
      flash[:notice] = "No matches found, please try another search"
      redirect_to :action => 'index'
    end
  end

  def search_by_author
    @author = session[:search_data][:object]
    @cites = session[:search_data][:cites]
    
    cond_string = ""
    cond_hash = {}
    
    unless first = @author[:firstname].blank?
      cond_string << "authors.firstname ILIKE :firstname "
      cond_hash[:firstname] = '%' + @author[:firstname].to_s + '%'
    end
    unless middle = @author[:middlename].blank?
      cond_string << (first ? "": "AND ") + "authors.middlename ILIKE :middlename "
      cond_hash[:middlename] = '%' + @author[:middlename].to_s + '%'
    end
    unless @author[:lastname].blank?
      cond_string << ((middle && first) ? "": "AND ") + "authors.lastname ILIKE :lastname "
      cond_hash[:lastname] = '%' + @author[:lastname].to_s + '%'
    end
    
    @pages, @collection = paginate Inflector.pluralize(Article.to_s).to_sym,
    :select => 'articles.*',
    :joins => "JOIN article_authors ON articles.id = article_authors.article_id JOIN authors ON" +
    " authors.id = article_authors.author_id",
    :conditions => [cond_string, cond_hash],
    :per_page => 10
  end

  def search_by_article
    @article = session[:search_data]
    @pages, @collection = paginate Inflector.pluralize(Article.to_s).to_sym,
    :conditions => ["articles.title ILIKE :title", { :title => '%' + @article[:title] + '%' }],
    :per_page => 10

    if @collection.empty?
      flash[:notice] = "No matches found, please try another search"
      redirect_to :action => 'index'
    end
  end

  def search_by_keyword
    @keyword = session[:search_data]
    @pages, @collection = paginate Inflector.pluralize(Article.to_s).to_sym,
    :select => 'articles.*',
    :joins => "JOIN article_keywords ON articles.id = article_keywords.article_id JOIN keywords ON" +
    " keywords.id = article_keywords.keyword_id",
    :conditions => ["keywords.name ILIKE :name", {:name => '%' + @keyword[:name] + '%'}],
    :per_page => 10

    if @collection.empty?
      flash[:notice] = "No matches found, please try another search"
      redirect_to :action => 'index'
    end
  end

  def search_cites
    @pages, @collection = paginate Inflector.pluralize(Article.to_s).to_sym,
    :select => 'articles.*',
    :joins => "JOIN cites ON articles.id = cites.cited_by_article_id",
    :conditions => "article_id = #{params[:id]}",
    :per_page => 10
  end
  
  def search_by_advanced
    @article = unless session[:search_data][:article].blank? then session[:search_data][:article] end
    @author = unless session[:search_data][:author].blank? then session[:search_data][:author] end 
    @keyword = unless session[:search_data][:keyword].blank? then session[:search_data][:keyword] end
    @busqueda = Array.new
    
    cond_string = ""
    cond_hash = {}
    joins = ""
    
    if @author
      unless first = @author[:firstname].blank?
        cond_string << "authors.firstname ILIKE :firstname "
        cond_hash[:firstname] = '%' + @author[:firstname].to_s + '%'
      end
      unless middle = @author[:middlename].blank?
        cond_string << (first ? "": " AND ") + "authors.middlename ILIKE :middlename "
        cond_hash[:middlename] = '%' + @author[:middlename].to_s + '%'
      end
      unless @author[:lastname].blank?
        cond_string << ((middle && first) ? "": " AND ") + "authors.lastname ILIKE :lastname "
        cond_hash[:lastname] = '%' + @author[:lastname].to_s + '%'
      end

      joins << " JOIN article_authors ON articles.id = article_authors.article_id " +
                "JOIN authors ON authors.id = article_authors.author_id "
    end
    
    if @article
      cond_string << (@author ? " AND " : "") + "articles.title ILIKE :title"
      cond_hash[:title] = "%" + @article[:title] + "%"
    end
    
    if @keyword
      cond_string << (!(@author && @article) ? "" : " AND ") + "keywords.name ILIKE :name"
      cond_hash[:name] = "%" + @keyword[:name] + "%"
      
      joins << "JOIN article_keywords ON articles.id = article_keywords.article_id " +
                "JOIN keywords ON keywords.id = article_keywords.keyword_id"
    end
    
    @pages, @collection = paginate Inflector.pluralize(Article.to_s).to_sym,
      :joins => joins,
      :conditions => [cond_string, cond_hash],
      :per_page => 10
    
    if @collection.empty?
      flash[:notice] = "No matches found, please try another search"
      redirect_to :action => 'index'
    end
  end


  def configure_search_form
    @form_fields = selected_fields params
  end
  
  def selected_fields hash
    selected = Array.new
    hash.keys.each do |key|
      if hash[key] == "1"
        selected << key
      end
    end
    selected
  end
  
end
