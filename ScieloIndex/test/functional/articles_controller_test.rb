require File.dirname(__FILE__) + '/../test_helper'
require 'articles_controller'

# Re-raise errors caught by the controller.
class ArticlesController; def rescue_action(e) raise e end; end

class ArticlesControllerTest < Test::Unit::TestCase
  fixtures :articles

  def setup
    @controller = ArticlesController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = articles(:article1).id
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
    num_articles = Article.count

    post :create, :record => {:id => 100, :title => 'Classification of thunderstorm and non-thunderstorm days in Calcutta (India) on the basis of linear discriminant analysis Second Part', :pages => '12 p.p', :url => 'http://scielo.unam.mx/scielo.php?script=sci_arttext&pid=S0187-62362004000100001&lng=es&nrm=iso&tlng=en', :pacsnum => '12 sss', :other => 'Atmósfera'}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_articles + 1, Article.count
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
      Article.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      Article.find(@first_id)
    }
  end
end
