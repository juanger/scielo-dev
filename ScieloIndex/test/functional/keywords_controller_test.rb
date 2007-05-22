require File.dirname(__FILE__) + '/../test_helper'
require 'keywords_controller'

# Re-raise errors caught by the controller.
class KeywordsController; def rescue_action(e) raise e end; end

class KeywordsControllerTest < Test::Unit::TestCase
  fixtures :keywords

  def setup
    @controller = KeywordsController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = keywords(:first).id
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

    assert_not_nil assigns(:keywords)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:keyword)
    assert assigns(:keyword).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:keyword)
  end

  def test_create
    num_keywords = Keyword.count

    post :create, :keyword => {}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_keywords + 1, Keyword.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:keyword)
    assert assigns(:keyword).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      Keyword.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      Keyword.find(@first_id)
    }
  end
end
