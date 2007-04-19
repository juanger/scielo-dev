require File.dirname(__FILE__) + '/../test_helper'
require 'journals_controller'

# Re-raise errors caught by the controller.
class JournalsController; def rescue_action(e) raise e end; end

class JournalsControllerTest < Test::Unit::TestCase
  fixtures :journals

  def setup
    @controller = JournalsController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = journals(:first).id
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

    assert_not_nil assigns(:journals)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:journal)
    assert assigns(:journal).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:journal)
  end

  def test_create
    num_journals = Journal.count

    post :create, :journal => {}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_journals + 1, Journal.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:journal)
    assert assigns(:journal).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      Journal.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      Journal.find(@first_id)
    }
  end
end
