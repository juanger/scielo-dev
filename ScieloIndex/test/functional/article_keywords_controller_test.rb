require File.dirname(__FILE__) + '/../test_helper'
require 'article_keywords_controller'

# Re-raise errors caught by the controller.
class ArticleKeywordsController; def rescue_action(e) raise e end; end

class ArticleKeywordsControllerTest < Test::Unit::TestCase
  fixtures :article_keywords, :users

  def setup
    @controller = ArticleKeywordsController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = article_keywords(:art1quakes).id
    @myarticle_keyword = {:article_id => 1, :keyword_id => 2}
    login_as(:quentin)
    
  end

  def test_index
    get :index
    assert_response :success
    assert_template 'list'
  end

  def test_list
    get :list

    assert_response :success
    assert_template 'list'

    assert_not_nil assigns(:collection)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:record)
    assert assigns(:record).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:record)
  end

  def test_create
    num_article_keywords = ArticleKeyword.count

    post :create, :record => @myarticle_keyword

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_article_keywords + 1, ArticleKeyword.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:record)
    assert assigns(:record).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      ArticleKeyword.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      ArticleKeyword.find(@first_id)
    }
  end
end
