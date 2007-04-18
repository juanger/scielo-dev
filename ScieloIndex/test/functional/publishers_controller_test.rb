require File.dirname(__FILE__) + '/../test_helper'
require 'publishers_controller'

# Re-raise errors caught by the controller.
class PublishersController; def rescue_action(e) raise e end; end

class PublishersControllerTest < Test::Unit::TestCase
  fixtures :publishers

  def setup
    @controller = PublishersController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = publishers(:first).id
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

    assert_not_nil assigns(:publishers)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:publisher)
    assert assigns(:publisher).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:publisher)
  end

  def test_create
    num_publishers = Publisher.count

    post :create, :publisher => {}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_publishers + 1, Publisher.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:publisher)
    assert assigns(:publisher).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      Publisher.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      Publisher.find(@first_id)
    }
  end
end
