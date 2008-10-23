require "#{File.dirname(__FILE__)}/../test_helper"

class InstitutionTest < ActionController::IntegrationTest
  fixtures :countries, :institutions
  def setup
    @institutions = [:unam, :ipn, :nyu]
  end

   def test_getting_index
     get "/institutions"
     assert_equal 200, status
     assert_equal '/institutions', path
   end

   def test_showing
     @institutions.each { | institution |
       post "/institutions/show", :id => institutions(institution).id
       assert 200, status
       assert_equal '/institutions/show', path
     }
   end

   def test_editing
     @institutions.each { | institution |
       post "/institutions/edit", :id => institutions(institution).id
       assert 200, status
       assert "/institutions/edit/#{institutions(institution).id}", path
     }
   end

   def test_updating_name
     @institutions.each { | institution |
       post "/institutions/update", :id => institutions(institution).id, :name => institutions(institution).name.reverse
       assert 302, status
       follow_redirect!
       assert "/institutions/show/#{institutions(institution).id}", path
     }
   end

    def test_deleting
     @institutions.each { | institution |
       get "/institutions/destroy", :id => institutions(institution).id
       assert 302, status
       assert '/institutions/list',  path
     }
   end
end
