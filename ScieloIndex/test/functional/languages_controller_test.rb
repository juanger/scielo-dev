require File.dirname(__FILE__) + '/../test_helper'
require 'languages_controller'

# Re-raise errors caught by the controller.
class LanguagesController; def rescue_action(e) raise e end; end

class LanguagesControllerTest < Test::Unit::TestCase
  fixtures :languages

  def setup
    @controller = LanguagesController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = languages(:first).id
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

    assert_not_nil assigns(:languages)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:language)
    assert assigns(:language).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:language)
  end

  def test_create
    num_languages = Language.count

    post :create, :language => {}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_languages + 1, Language.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:language)
    assert assigns(:language).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      Language.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      Language.find(@first_id)
    }
  end
end
