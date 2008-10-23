require File.dirname(__FILE__) + '/../test_helper'
require 'article_authors_controller'

# Re-raise errors caught by the controller.
class ArticleAuthorsController; def rescue_action(e) raise e end; end

class ArticleAuthorsControllerTest < Test::Unit::TestCase
  fixtures :journals, :journal_issues, :articles, :authors, :article_authors, :users

  def setup
    @controller = ArticleAuthorsController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = article_authors(:monoart2).id
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
    num_article_authors = ArticleAuthor.count

    post :create, :record => {:author_id => 2, :article_id => 1, :author_order => 2}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_article_authors + 1, ArticleAuthor.count
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
      ArticleAuthor.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      ArticleAuthor.find(@first_id)
    }
  end
end
