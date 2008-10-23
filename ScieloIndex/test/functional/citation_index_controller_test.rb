require File.dirname(__FILE__) + '/../test_helper'
require 'citation_index_controller'

# Re-raise errors caught by the controller.
class CitationIndexController; def rescue_action(e) raise e end; end

class CitationIndexControllerTest < Test::Unit::TestCase
  def setup
    @controller = CitationIndexController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new
  end

  # Replace this with your real tests.
  def test_truth
    assert true
  end
end
